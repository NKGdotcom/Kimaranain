using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Linq;


public class Roomba : MonoBehaviour
{
    [Header("NavMeshAgent")]
    [SerializeField] private NavMeshAgent agent;

    [Header("�����o�̈ړ��X�s�[�h")]
    [SerializeField] private float roombaMoveSpeed;

    [Header("�����o�̉�]�X�s�[�h")]
    [SerializeField] private float roombaRotationSpeed;

    [Header("�ړI�n���B�̋��e����")]
    [SerializeField] private float destinationThreshold = 0.5f; // Agent���ړI�n�ɓ��B�����Ƃ݂Ȃ�����

    [Header("�o�H�̎��")]
    [SerializeField] private RouteSettings[] routeSettings;

    private int routeNum; // 1�̌o�H�T���͉����[�g���邩
    private int routeSettingsNum; // �ݒ肵�����[�g�̎�ނ̐�
    private int routeSetting; // random�ɑI�΂ꂽ���[�g�̎��
    private int nowRouteID; // ���݂̃��[�g�͉��ڂ̕����ɂ��邩

    private List<float> distanceDestination = new List<float>();

    private bool first = true;
    private bool isOnSwitch = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inspector��agent���A�T�C������Ă��邩�m�F
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent������GameObject�Ɍ�����Ȃ����AInspector�ŃA�T�C������Ă��܂���B");
                enabled = false; // �G�[�W�F���g��������Ȃ��ꍇ�̓X�N���v�g�𖳌���
                return;
            }
        }

        agent.speed = roombaMoveSpeed;
        agent.angularSpeed = roombaRotationSpeed;
        agent.autoBraking = true; // Roomba���ړI�n�ɋ߂Â��Ǝ����I�Ɍ����E��~����悤�ݒ�
        agent.updateRotation = true; // NavMeshAgent�ɉ�]��C����

        routeSettingsNum = routeSettings.Length;
        if (routeSettingsNum == 0)
        {
            Debug.LogError("�o�H�ݒ肪��`����Ă��܂���B�����o�͈ړ��ł��܂���B");
            enabled = false;
            return;
        }

        // Random.Range(int min, int max)��max���r���I�Ȃ̂ŁArouteSettingsNum�𒼐ڎw��
        routeSetting = 0;
        // �I�����ꂽ�o�H�ݒ肪�L���ł���A�ړ��|�C���g�����邱�Ƃ��m�F
        if (routeSetting >= routeSettingsNum || routeSettings[routeSetting].MovePointList.Length == 0)
        {
            Debug.LogError("�I�����ꂽ�����_���Ȍo�H�ݒ肪�����ł��邩�A�ړ��|�C���g������܂���B");
            enabled = false;
            return;
        }

        routeNum = routeSettings[routeSetting].RouteNum;

        GetMinDistance(); //��ԋ߂��ꏊ���擾
        SetNextDestination(); // �ŏ��̖ړI�n��ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        // �G�[�W�F���g���p�X���v�Z���łȂ��A�ړI�n�ɏ\���߂Â��Ă��邩�`�F�b�N
        if (!agent.pathPending && agent.remainingDistance < destinationThreshold)
        {
            // �G�[�W�F���g����~���Ă��邩�A�܂��̓^�[�Q�b�g�ɔ��ɋ߂��ꍇ�A���̃|�C���g�ֈړ�
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                SetNextDestination();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            isOnSwitch = false;
            SetNextDestination();
        }
        if (Input.GetKeyDown(KeyCode.H)) ChangeRouteSetting();
    }

    /// <summary>
    /// ���̖ړI�n��ݒ�
    /// </summary>
    private void SetNextDestination()
    {
        if (routeSettings == null || routeSettings.Length == 0) return;

        // �V�[�P���X���̎��̃|�C���g�ֈړ�
        if (!first && isOnSwitch) nowRouteID++; //�X�C�b�`���I��
        else if (!first && !isOnSwitch) nowRouteID--; //�X�C�b�`���I�t

        if (nowRouteID >= routeNum) nowRouteID = 0; //�Ō�̃|�C���g�ɓ��B������ŏ��̏ꏊ��
        if (nowRouteID < 0) nowRouteID = routeNum; //�ŏ��̃|�C���g�ɓ��B������Ō�̏ꏊ��

        Vector3 nextDestination = routeSettings[routeSetting].MovePointList[nowRouteID].position;
        agent.SetDestination(nextDestination);
        Debug.Log($"���̖ړI�n��ݒ肵�܂���: {nextDestination}");
        if(first) first = false;
    }
    /// <summary>
    ///���[�g�ύX
    /// </summary>
    private void ChangeRouteSetting()
    {
        routeSetting++;�@//�����͍���ς��\����
        routeNum = routeSettings[routeSetting].RouteNum;
        GetMinDistance();
        nowRouteID--;
    }
    /// <summary>
    /// �ړI�n�̈�ԋ߂��ꏊ���擾
    /// </summary>
    private void GetMinDistance()
    {
        distanceDestination.Clear();
        for(int i =0; i < routeNum; i++)
        {
            distanceDestination.Add(Distance(this.gameObject.transform.position, routeSettings[routeSetting].MovePointList[i].position));
        }
        float _minDistance = distanceDestination.Min();
        int _minIndex = distanceDestination.IndexOf(_minDistance);
        nowRouteID = _minIndex;
    }
    /// <summary>
    /// �����𑪂�
    /// </summary>
    /// <param name="_roombaPos"></param>
    /// <param name="_destinationPos"></param>
    /// <returns></returns>
    public float Distance(Vector3 _roombaPos, Vector3 _destinationPos)
    {
        Vector3 _startingPoint = _roombaPos;
        Vector3 _endPoint = _destinationPos;

        return Vector3.Distance(_startingPoint, _endPoint);
    }
    /// <summary>
    /// �o�H�̒n�_�Ǘ��N���X
    /// </summary>
    [System.Serializable]
    public class RouteSettings
    {
        [Header("�����o�̌o�H")]
        [SerializeField] private Transform[] movePointList; //�n�_�̊i�[
        public Transform[] MovePointList { get => movePointList; private set => movePointList = value; }
        public int RouteNum { get => movePointList.Length;}
    }
}

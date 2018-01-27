using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace qwe
{
    public class TaskProgressManager : MonoBehaviour
    {

        private TaskProgressLevel1_8 _taskProgressLevel1_8;
        private TaskProgressLevel9_14 _taskProgressLevel9_14;
        private TaskProgressLevel15_19 _taskProgressLevel15_19;
        private TaskProgressLevel20_21 _taskProgressLevel20_21;
        private TaskProgressLevel22_26 _taskProgressLevel22_26;
        private TaskProgressLevel27 _taskProgressLevel27;
        private TaskProgressLevel28 _taskProgressLevel28;

        public void Init()
        {
            _taskProgressLevel1_8 = this.gameObject.AddComponent<TaskProgressLevel1_8>();
            _taskProgressLevel1_8.taskProgressManager = this;
            _taskProgressLevel9_14 = this.gameObject.AddComponent<TaskProgressLevel9_14>();
            _taskProgressLevel9_14.taskProgressManager = this;
            _taskProgressLevel15_19 = this.gameObject.AddComponent<TaskProgressLevel15_19>();
            _taskProgressLevel15_19.taskProgressManager = this;
            _taskProgressLevel20_21 = this.gameObject.AddComponent<TaskProgressLevel20_21>();
            _taskProgressLevel20_21.taskProgressManager = this;
            _taskProgressLevel22_26 = this.gameObject.AddComponent<TaskProgressLevel22_26>();
            _taskProgressLevel22_26.taskProgressManager = this;
            _taskProgressLevel27 = this.gameObject.AddComponent<TaskProgressLevel27>();
            _taskProgressLevel27.taskProgressManager = this;
            _taskProgressLevel28 = this.gameObject.AddComponent<TaskProgressLevel28>();
            _taskProgressLevel28.taskProgressManager = this;
        }

        public void EnterTask(int taskId)
        {
            int tempId = ConvertTaskId(taskId);

            if (tempId < 9000)
            {
                _taskProgressLevel1_8.EnterTask(taskId);
            }
            else if (tempId < 15000)
            {
                _taskProgressLevel9_14.EnterTask(taskId);
            }
            else if (tempId < 20000)
            {
                _taskProgressLevel15_19.EnterTask(taskId);
            }
            else if (tempId < 22000)
            {
                _taskProgressLevel20_21.EnterTask(taskId);
            }
            else if (tempId < 27000)
            {
                _taskProgressLevel22_26.EnterTask(taskId);
            }
            else if (tempId < 28000)
            {
                _taskProgressLevel27.EnterTask(taskId);
            }
            else if (tempId < 29000)
            {
                _taskProgressLevel28.EnterTask(taskId);
            }
        }

        public void UpdateTask(int taskId)
        {
            int tempId = ConvertTaskId(taskId);

            if (tempId < 9000)
            {
                _taskProgressLevel1_8.UpdateTask(taskId);
            }
            else if (tempId < 15000)
            {
                _taskProgressLevel9_14.UpdateTask(taskId);
            }
            else if (tempId < 20000)
            {
                _taskProgressLevel15_19.UpdateTask(taskId);
            }
            else if (tempId < 22000)
            {
                _taskProgressLevel20_21.UpdateTask(taskId);
            }
            else if (tempId < 27000)
            {
                _taskProgressLevel22_26.UpdateTask(taskId);
            }
            else if (tempId < 28000)
            {
                _taskProgressLevel27.UpdateTask(taskId);
            }
            else if (tempId < 29000)
            {
                _taskProgressLevel28.UpdateTask(taskId);
            }
        }

        public void ExitTask(int taskId)
        {
            int tempId = ConvertTaskId(taskId);

            if (tempId < 9000)
            {
                _taskProgressLevel1_8.ExitTask(taskId);
            }
            else if (tempId < 15000)
            {
                _taskProgressLevel9_14.ExitTask(taskId);
            }
            else if (tempId < 20000)
            {
                _taskProgressLevel15_19.ExitTask(taskId);
            }
            else if (tempId < 22000)
            {
                _taskProgressLevel20_21.ExitTask(taskId);
            }
            else if (tempId < 27000)
            {
                _taskProgressLevel22_26.ExitTask(taskId);
            }
            else if (tempId < 28000)
            {
                _taskProgressLevel27.ExitTask(taskId);
            }
            else if (tempId < 29000)
            {
                _taskProgressLevel28.ExitTask(taskId);
            }
        }

        private int ConvertTaskId(int taskId)
        {
            if (taskId < 28999)
            {
                return taskId;
            }
            else
            {
                return Mathf.FloorToInt(taskId / 10.0f);
            }
        }
    }
}
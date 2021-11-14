using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EggNamespace.UI.View
{
    public abstract class ViewControllerBase : MonoBehaviour
    {
        public UnityEvent onOpenViewEvent;
        public UnityEvent onCloseViewEvent;
        public virtual void OpenView() { onOpenViewEvent?.Invoke(); }
        public virtual void CloseView() { onCloseViewEvent?.Invoke(); }
    }
}
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor.Events;

namespace Dutchskull.Utilities.Extensions
{
    public static class UnityEventExtension
    {
        public static void AddPersistentListener(this UnityEvent uEvent, Action action)
        {
            MethodInfo targetInfo = UnityEventBase.GetValidMethodInfo(action.Target, action.Method.Name, new Type[0]);
            UnityAction methodDelegate = Delegate.CreateDelegate(typeof(UnityAction), action.Target, targetInfo) as UnityAction;
            UnityEventTools.AddPersistentListener(uEvent, methodDelegate);
        }

        public static void RemoveAllPresistentListener(this Button button)
        {
            int count = button.onClick.GetPersistentEventCount();

            if (count <= 0)
                return;

            for (int i = count - 1; i >= 0; i--)
                button.RemovePresistentListener(i);
        }

        public static void RemovePresistentListener(this Button button, int index)
        {
            if (button.onClick.GetPersistentEventCount() <= 0)
                return;

            UnityEventTools.RemovePersistentListener(button.onClick, index);
        }
    }
}
#endif

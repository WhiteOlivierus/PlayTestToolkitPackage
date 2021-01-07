using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTestToolkit.Runtime.Data
{
    [Serializable]
    public class PlayTestCollection : ScriptableObject
    {
        [SerializeField]
        private string title = string.Empty;
        public string Title { get => title; set => title = value; }

        [SerializeField]
        private List<PlayTest> playtests = new List<PlayTest>();
        public IList<PlayTest> Playtests { get => playtests; set => playtests = (List<PlayTest>)value; }

        [SerializeField]
        private bool fold = true;
        public bool Fold { get => fold; set => fold = value; }

    }
}

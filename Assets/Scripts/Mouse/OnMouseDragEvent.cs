﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnMouseDragEvent : UnityEvent<Vector2, Vector2> {}

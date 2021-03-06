﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphConnectivity.Core.Models;

namespace GraphConnectivity.Core.Services
{
    public interface IGraphVizualiser
    {
        byte[] VisualiseGraph<T>(Graph<T> graph);
    }
}

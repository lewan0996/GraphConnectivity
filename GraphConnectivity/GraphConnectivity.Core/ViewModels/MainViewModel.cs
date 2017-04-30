﻿using System.Linq;
using GraphConnectivity.Core.Models;
using GraphConnectivity.Core.Services;
using MvvmCross.Core.ViewModels;

namespace GraphConnectivity.Core.ViewModels
{
    internal class MainViewModel : MvxViewModel
    {
        private readonly IGraphVizualiser _graphVizualiser;
        private readonly Graph<string> _graph;

        public MainViewModel(IGraphVizualiser graphVizualiser)
        {
            _graphVizualiser = graphVizualiser;
            _graph = new Graph<string>();
        }

        private string _newVertexValue;

        public string NewVertexValue
        {
            get { return _newVertexValue; }
            set
            {
                _newVertexValue = value;
                RaisePropertyChanged(() => NewVertexValue);
            }
        }

        private string _newEdgeFromValue;

        public string NewEdgeFromValue
        {
            get { return _newEdgeFromValue; }
            set
            {
                _newEdgeFromValue = value;
                RaisePropertyChanged(() => NewEdgeFromValue);
            }
        }

        private string _newEdgeToValue;

        public string NewEdgeToValue
        {
            get { return _newEdgeToValue; }
            set
            {
                _newEdgeToValue = value;
                RaisePropertyChanged(() => NewEdgeToValue);
            }
        }

        private MvxCommand _addVertexCommand;

        public MvxCommand AddVertexCommand
        {
            get
            {
                _addVertexCommand = _addVertexCommand ?? new MvxCommand(AddVertexHandler);
                return _addVertexCommand;
            }
        }

        private MvxCommand _removeVertexCommand;

        public MvxCommand RemoveVertexCommand
        {
            get
            {
                _removeVertexCommand = _removeVertexCommand ?? new MvxCommand(RemoveVertexHandler);
                return _removeVertexCommand;
            }
        }


        private MvxCommand _addEdgeCommand;

        public MvxCommand AddEdgeCommand
        {
            get
            {
                _addEdgeCommand = _addEdgeCommand ?? new MvxCommand(AddEdgeHandler);
                return _addEdgeCommand;
            }
        }

        private MvxCommand _removeEdgeCommand;

        public MvxCommand RemoveEdgeCommand
        {
            get
            {
                _removeEdgeCommand = _removeEdgeCommand ?? new MvxCommand(RemoveEdgeHandler);
                return _removeEdgeCommand;
            }
        }

        private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; RaisePropertyChanged(() => IsConnected); }
        }

        private byte[] _imageBytes;

        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set { SetProperty(ref _imageBytes, value); }
        }

        private void RefreshVisualisation()
        {
            ImageBytes = _graphVizualiser.VisualiseGraph(_graph);

            NewEdgeFromValue = "";
            NewEdgeToValue = "";
            NewVertexValue = "";
        }

        private void AddVertexHandler()
        {
            _graph.AddVertex(NewVertexValue);
            RefreshVisualisation();
        }

        private void RemoveVertexHandler()
        {
            _graph.RemoveVertexByValue(NewVertexValue);
            RefreshVisualisation();
        }

        private void AddEdgeHandler()
        {
            _graph.AddEdgeByValues(NewEdgeFromValue, NewEdgeToValue);
            RefreshVisualisation();
        }

        private void RemoveEdgeHandler()
        {
            _graph.RemoveEdgeByValues(NewEdgeFromValue, NewEdgeToValue);
            RefreshVisualisation();
        }
    }
}
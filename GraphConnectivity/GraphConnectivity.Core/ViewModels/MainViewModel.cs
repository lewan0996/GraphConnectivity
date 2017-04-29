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
            this._graphVizualiser = graphVizualiser;
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


       /* private MvxCommand _refreshVisualisationCommand;

        public MvxCommand RefreshVisualisationCommand
        {
            get
            {
                _refreshVisualisationCommand = _refreshVisualisationCommand ?? new MvxCommand(RefreshVisualisation);
                return _refreshVisualisationCommand;
            }

        }*/

        private MvxCommand _addVertexCommand;

        public MvxCommand AddVertexCommand
        {
            get
            {
                _addVertexCommand = _addVertexCommand ?? new MvxCommand(AddVertexHandler);
                return _addVertexCommand;
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
        }

        private void AddVertexHandler()
        {
            _graph.AddVertex(NewVertexValue);
            RefreshVisualisation();
        }
    }
}

using System;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private const int iteratorVersion = 1;
        private readonly GameSaveLoader _saveLoader;
        private readonly ControlsModel _controlsModel;

        public ControlsPresenter(GameSaveLoader saveLoader, ControlsModel controlsModel)
        {
            _saveLoader = saveLoader;
            _controlsModel = controlsModel;
        }

        public async void Save(Action<bool, int> callback)
        {
            int currentVersion = _controlsModel.LoadVersion();
            
            bool value = await _saveLoader.Save(currentVersion + iteratorVersion);

            if (value)
            {
                callback.Invoke(value, currentVersion + iteratorVersion);
                _controlsModel.SaveVersion(currentVersion + iteratorVersion);
            }
            else
            {
                callback.Invoke(value, currentVersion + iteratorVersion);
            }
        }

        public async void Load(string versionText, Action<bool, int> callback)
        {
            int.TryParse(versionText, out int result);
            if (result != 0)
            {
                bool value = await _saveLoader.Load(result);
                if (value)
                {
                    callback.Invoke(true, result);
                }
                else
                {
                    callback.Invoke(false, result);
                }
            }                        
        }
    }
}
namespace BC7
{
    public partial class GlobalGame
    {
        private Scene CreateMenue()
        {
            Scene menue = new Scene();
            MenueUI menueUI = new MenueUI(uiAssets, input.Keys,
                id => ChangeScene(GetScene(id)), MaxSceneID, creator);
            menue.AddRange(
                menueUI,
                new VersionDisplay(uiAssets)
            );
            return menue;
        }
    }
}
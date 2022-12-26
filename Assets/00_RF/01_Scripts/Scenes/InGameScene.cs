public class InGameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        // 씬타입 지정
        SceneType = Define.Scene.InGame;
        // 인벤토리 UI
        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void Clear()
    {
    }
}

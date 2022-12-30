public class InGameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        // 씬타입 지정
        SceneType = Define.Scene.InGame;
        // 인벤토리 UI
        Managers.UI.ShowSceneUI<UI_Inven>();
        
        for (int i = 0; i < 5; i++)
        {
            Managers.Resource.Instantiate("Player");
        }
    }

    public override void Clear()
    {
    }
}

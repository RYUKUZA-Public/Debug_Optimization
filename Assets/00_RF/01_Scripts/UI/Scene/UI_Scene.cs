public class UI_Scene : UI_Base
{
    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}

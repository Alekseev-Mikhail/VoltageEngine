namespace Client;

public interface IViewport
{
    public void OnSetup(Render render, uint width, uint height);
    
    public void OnRender(Render render, uint width, uint height);
}
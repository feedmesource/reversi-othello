using BLL;

namespace ConsoleApp;

public static class Variables
{
    public static GameConfiguration Config;

    static Variables()
    {
        Config = new GameConfiguration();
    }
}
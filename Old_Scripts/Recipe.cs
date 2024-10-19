using Godot;

public class Recipe 
{
    Tool AssignedTool = null;
    int Id = -1;

    public Recipe(int id, Tool tool)
    {
        Id = id;
        AssignedTool = tool;
    }

    public void SetTool(Tool newTool)
    {
        AssignedTool = newTool;
    }

    public Tool GetTool()
    {
        return AssignedTool;
    }

    public int GetId()
    {
        return Id;
    }
}
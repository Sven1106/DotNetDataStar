namespace Starfederation.Datastar.MinimalBlazor.Components
{
    public partial class Index
    {
        public record Props(string Title, string Description)
        {
            public static implicit operator Dictionary<string, object>(Props props) => new()
            {
                ["Title"] = props.Title, 
                ["Description"] = props.Description
            };
        }
    }
}
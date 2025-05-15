namespace Empty.Components
{
    public partial class Index
    {
        public record Props(string Title, string Description)
        {
            public static implicit operator Dictionary<string, object>(Props props) => new()
            {
                [nameof(props.Title)] = props.Title, 
                [nameof(props.Description)] = props.Description
            };
        }
    }
    public partial class DisplayDate
    {
        public record Props(DateTime Today)
        {
            public static implicit operator Dictionary<string, object>(Props props) => new()
            {
                [nameof(props.Today)] = props.Today
            };
        }
    }
}
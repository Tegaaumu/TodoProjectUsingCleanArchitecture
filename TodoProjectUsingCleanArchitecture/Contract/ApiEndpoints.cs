namespace TodoProjectUsingCleanArchitecture.Contract
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Identity
        {
            public const string Login = $"{ApiBase}/identity/login";
        }

        public static class Register
        {
            public const string RegisterUser = $"{ApiBase}/register";
        }

        public static class TodoList
        {
            public const string BaseRoute = $"{ApiBase}/todolist";
            public const string Get = BaseRoute;
            public const string GetById = $"{BaseRoute}/{{id}}";
            public const string Create = BaseRoute;
            public const string Update = $"{BaseRoute}/{{id}}";
            public const string Delete = $"{BaseRoute}/{{id}}";
        }

        public static class Permissions
        {
            public const string Assign = $"{ApiBase}/permissions/assign";
        }

        public static class Policies
        {
            public const string CanCreate = "CanCreate";
            public const string CanEdit = "CanEdit";
            public const string CanDelete = "CanDelete";
            public const string CanAssign = "CanAssign";
        }
    }
}

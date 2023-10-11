namespace UsersManager_BAL
{
    public static class Constants
    {
        public static string NotFoundEntityByIdErrorMessage => "Entity with current id not found";
        public static string UserWithCurrentEmailAlreadyExist => "User with current email already exist";
        public static string TryToAddErrorMessage => "Error when try to add entity";
        public static string TryToUpdateErrorMessage => "Error when try to update entity";
        public static string TryToDeleteErrorMessage => "Error when try to delete entity";
        public static string InvalidRefreshToken => "Invalid refresh token";
        public static string LoginOrPasswordIsWrong => "Login or password is wrong";


        public static string TokenEncryptKey => "f03b812ff91c4b4089dd8c38b8439474";
        public static string AuthenticationScheme => "JwtBearerAuthentication";
    }
}
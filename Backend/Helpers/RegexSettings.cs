namespace PruebaTecnicaUniversal.Helpers
{
    /// <summary>
    /// Contiene patrones de expresiones regulares para validaciones comunes.
    /// </summary>
    public static class RegexSettings
    {
        /// <summary>
        /// Patrón para validar correos electrónicos.
        /// Formato básico:: algo@dominio.extension
        /// </summary>
        public const string EmailPattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        /// <summary>
        /// Patrón para validar contraseñas seguras.
        /// Requisitos:
        /// - Al menos una letra minúscula
        /// - Al menos una letra mayúscula
        /// - Al menos un número
        /// - Al menos un carácter especial
        /// - Mínimo 8 caracteres
        /// </summary>
        public const string PasswordPattern =
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
    }
}

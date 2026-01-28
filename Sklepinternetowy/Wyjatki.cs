using System;

namespace Sklepinternetowy
{
    public class WrongPeselException : Exception
    {
        public WrongPeselException(string message) : base(message) { }
    }

    public class BledneDaneException : Exception
    {
        public BledneDaneException(string message) : base(message) { }
    }

    public class ProduktJuzIstniejeException : Exception
    {
        public ProduktJuzIstniejeException(string kod)
            : base($"Produkt o kodzie '{kod}' już istnieje w asortymencie.") { }
    }

    public class PracownikJuzIstniejeException : Exception
    {
        public PracownikJuzIstniejeException(string pesel)
            : base($"Pracownik z PESEL {pesel} już znajduje się w zespole.") { }
    }
}
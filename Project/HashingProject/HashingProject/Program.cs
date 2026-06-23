//List<string> passwords = [
//    "uzWC67",
//    "2L6KZG",
//    "JlFRCZ",
//    "8ntwUp",
//    "YOyhfR",
//    "RSbvHv",
//    "rwVDh9",
//    "LdNyos",
//    "gynQMT",
//    "AtnDjr"
//];

//List<string> hashPasswords = [
//    "$2a$11$ziBtXu./odrXbm62Gam5EepLA.Ha2/o5wEA1d899a55WPqs8IfXdy",
//    "$2a$11$Fwvaxl4wiUcqQBKLlk9o8usWVWcurf/nAIG1rJx3EIJyZERHQp4vW",
//    "$2a$11$nlqBl7UeAztiaMkiTfb.gOraw49BZ8U2QxwSCrdzB7HxB3V1Vhj9C",
//    "$2a$11$EU7RQyEAkUJjwzvFAxIlDuMP/ozObQSwjAVlqvnf3SLTnAMNGLgR.",
//    "$2a$11$Gmc/4guoMWBaTXhu33PuLO3VDan59hCPR2IAUHMhJubQ8nL0O9bii",
//    "$2a$11$5G98MMxi5Efw9sbs5I142.8Li.71YOPISVD93yxkq7tP9Dfl.Ke.G",
//    "$2a$11$ZfNvo3oKAiw09vtyOyFbxeXM1dK4Em/fk6rb29P5xDG1QXPs7XwNC",
//    "$2a$11$4dAWkcmKP/Kd1ppn9Xyy8etyQ.TsTXV0Z0OCzh5VqQyHj2bg.2lBi",
//    "$2a$11$dcPLe/HsNb15cxBKoKznJeYVArAk98Yk2APlcI3zBfDlkwe7.Cs5i",
//    "$2a$11$lfzmanrtX8McQ1L9CLx/OeFoFGAQqsxZXW2duk2KpFp1Ox.EsF7JO",
//];

//foreach (string password in passwords)
//    Console.WriteLine(HashPassword(password));

//static string HashPassword(string password)
//{
//    return BCrypt.Net.BCrypt.HashPassword(password);
//}


Console.WriteLine(isRight());

bool isRight()
{
    return BCrypt.Net.BCrypt.Verify("uzWC67", "$2a$11$ziBtXu./odrXbm62Gam5EepLA.Ha2/o5wEA1d899a55WPqs8IfXdy");
}
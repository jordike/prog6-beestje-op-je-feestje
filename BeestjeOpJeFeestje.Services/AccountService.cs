using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Services;

public class AccountService
{
    private readonly UserManager<Account> _userManager;

    // This is a string containing all the characters that can be used in a password.
    // This is used to generate a random password for the user.
    private const string AlphaNumericCharacters = "abcdefghijklmnopqrstuvwxyz";
    private const string NonAlphaNumericCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";

    private const int NonAlphaNumericCharactersLength = 2;
    private const int AlphaNumericCharactersLength = 6;

    public AccountService(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public List<Account> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }

    public async Task<Account?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<Account?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task DeleteUser(Account account)
    {
        await _userManager.DeleteAsync(account);
    }

    public async Task UpdateUser(Account account)
    {
        await _userManager.UpdateAsync(account);
    }

    public async Task<Tuple<string, IdentityResult>> CreateUser(string name, string email, string adres, string phoneNumber, MembershipLevel membershipLevel)
    {
        Account user = new Account
        {
            UserName = NormalizeUserName(name),
            Name = name,
            Email = email,
            Address = adres,
            PhoneNumber = phoneNumber,
            MembershipLevel = membershipLevel,
        };
        string password = GeneratePassword();

        IdentityResult result = await _userManager.CreateAsync(user, password);

        return new Tuple<string, IdentityResult>(password, result);
    }

    public async Task AssignRole(Account user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    private string GeneratePassword()
    {
        Random random = new Random();
        List<char> passwordCharacters = new List<char>();

        // Add required non-alphanumeric characters
        for (int i = 0; i < NonAlphaNumericCharactersLength; i++)
        {
            passwordCharacters.Add(NonAlphaNumericCharacters
                .OrderBy(c => random.Next())
                .First());
        }

        // Add required alphanumeric characters
        // -1 because we add one uppercase character later
        for (int i = passwordCharacters.Count; i < AlphaNumericCharactersLength - 1; i++)
        {
            passwordCharacters.Add(AlphaNumericCharacters[random.Next(AlphaNumericCharacters.Length)]);
        }

        // Add one random uppercase character
        passwordCharacters.Add(char.ToUpper(AlphaNumericCharacters[random.Next(AlphaNumericCharacters.Length)]));

        // Add random digits to the password
        passwordCharacters.Add(random.Next(10).ToString()[0]);

        // Shuffle the characters to ensure randomness
        passwordCharacters = passwordCharacters.OrderBy(c => random.Next()).ToList();

        return new string(passwordCharacters.ToArray());
    }

    private string NormalizeUserName(string name)
    {
        return name.Replace(' ', '_');
    }
}

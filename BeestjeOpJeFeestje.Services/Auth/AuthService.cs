using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Services.Auth;

public class AuthService
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;

    // This is a string containing all the characters that can be used in a password.
    // This is used to generate a random password for the user.
    private const string AlphaNumericCharacters = "abcdefghijklmnopqrstuvwxyz";
    private const string NonAlphaNumericCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";

    private const int NonAlphaNumericCharactersLength = 2;
    private const int AlphaNumericCharactersLength = 6;

    public AuthService(UserManager<Account> userManager, SignInManager<Account> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(string email, string password)
    {
        Account? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        SignInResult result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string?> CreateUser(string name, string email, string adres, string phoneNumber, Membershiplevel membershipLevel)
    {
        Account user = new Account
        {
            UserName = name,
            Name = name,
            Email = email,
            Address = adres,
            PhoneNumber = phoneNumber,
            MembershipLevel = membershipLevel,
        };
        string password = GeneratePassword();

        IdentityResult result = await _userManager.CreateAsync(user, password);

        return result.Succeeded
            ? password
            : null;
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
}

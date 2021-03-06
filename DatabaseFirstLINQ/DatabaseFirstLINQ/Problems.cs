using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            BonusTwo();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            var users = _context.Users;
            // HINT: .ToList().Count
            int count = users.Count();
            Console.WriteLine(count);
           

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            var products = _context.Products;
            foreach (Product product in products)
            {
                if (product.Price > 150)
                {
                    // Then print the name and price of each product from the above query to the console.
                    Console.WriteLine(product.Name);
                }
            }
            

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            var products = _context.Products;
            foreach (Product product in products)
            {
                if (product.Name.ToLower().Contains("s"))
                {
                    // Then print the name of each product from the above query to the console.
                    Console.WriteLine(product.Name);
                }
            }
            

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            DateTime specifiedData = DateTime.Parse("1/1/2016");
            var users = _context.Users;
            foreach (User user in users)
            {
                if (user.RegistrationDate < specifiedData)
                {
                    // Then print each user's email and registration date to the console.
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.RegistrationDate);
                }
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            DateTime beforeDate = DateTime.Parse("1/1/2018");
            DateTime afterDate = DateTime.Parse("12/31/2016");
            var users = _context.Users;
            foreach (User user in users)
            {
                if (user.RegistrationDate > afterDate && user.RegistrationDate < beforeDate)
                {
                    // Then print each user's email and registration date to the console.
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.RegistrationDate);
                }
            }    
        }
        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.

            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles
                .Include(ur => ur.Role)
                .Include(ur => ur.User)
                .Where(r => r.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            var cartProduct = _context.ShoppingCarts
                .Include(sc => sc.Product)
                .Include(sc => sc.User)
                .Where(u => u.User.Email == "afton@gmail.com");
                // Then print the product's name, price, and quantity to the console.
            foreach(ShoppingCart cart in cartProduct)
            {
                Console.WriteLine($"Product: {cart.Product.Name} Price: {cart.Product.Price} Quantity: {cart.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            var cartProduct = _context.ShoppingCarts
                .Include(sc => sc.Product)
                .Include(sc => sc.User)
                .Where(u => u.User.Email == "oda@gmail.com")
                .Select(sc => sc.Product.Price).Sum();
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            Console.WriteLine(cartProduct);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            var employees = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .Where(u => u.Role.RoleName == "Employee")
                .Select(ur => ur.UserId);
            Console.WriteLine(employees);
            var cartProduct = _context.ShoppingCarts
                .Include(sc => sc.Product)
                .Include(sc => sc.User)
                .Where(sc => employees.Contains(sc.UserId));
            foreach(var product in cartProduct)
            {
                Console.WriteLine($"User Email: {product.User.Email} Product: {product.Product.Name} Price: {product.Product.Price} Quantity: {product.Quantity}");
            }

            // Then print the user's email as well as the product's name, price, and quantity to the console.
            Console.WriteLine(cartProduct);
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "IBM",
                Description = "Computer",
                Price = 500
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "IBM").Select(p => p.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "IBM").SingleOrDefault();
            product.Price = 750;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(u => u.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();


        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();


            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();


            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            // Take the email and password and check if the there is a person that matches that combination.
            var userEmail = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            var userPassword = _context.Users.Where(u => u.Password == password).SingleOrDefault();
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            if (userEmail != null && userPassword != null)
            {
                Console.WriteLine("Signed In!");
            }
            else {
                Console.WriteLine("Invalid Email or Password");
            };
         
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            var totalCartPrice = _context.ShoppingCarts
                .Include(sc => sc.Product)
                .Include(sc => sc.User)
                .Where(sc => sc.User.Id != null)
                .Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(totalCartPrice.ToString());
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            foreach (ShoppingCart Cart in _context.ShoppingCarts)
            {
                int user = Cart.User.Id;
                if (Cart.User.Id == user)
                {
                    var userTotal = _context.ShoppingCarts
                    .Where(sc => sc.User.Id == user)
                    .Select(sc => sc.Product.Price).Sum();
                    Console.WriteLine($"User with ID: {user} total cart price is {userTotal}");
                }
                else { user = Cart.User.Id; }
            }
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

        }

    }
}

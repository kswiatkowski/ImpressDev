# ImpressDev - BookShop in ASP.NET MVC
**by kswiatkowski**

 - LinkedIn: [linkedin.com/in/kswiatkowski](www.linkedin.com/in/kswiatkowski)
 - Email: kaswiatkowski@gmail.com

# Live Demo:
http://impressdev.somee.com/

![](Screenshots/screenshot%20(1).png)

# Features:

**Product catalog:**
 - cache - downloading a list of categories, news and bestsellers (24h cache) (home page)
 - asynchronous search results filter (ajax)
 - search suggestions (data-autocomplete) (ajax)
 - BreadCrumbs (MvcSiteMapProvider) - path on each subpage
 
**Shopping:**
 - adding items to the cart before logging in (remembering after logging in)
 - asynchronous change the number of cart items in navbar (ajax)
 - asynchronous management of the contents of the cart (removal without reloading the page) (ajax)
 - dynamic calculation of the order value depending on the contents of the cart (ajax)
 - dynamic display of relevant messages after cleaning the cart (ajax)
 - forwarding user data (account details) in the order form fields (and saving order data to account settings)

**User's account:**
 - registration and login system with data encryption (ASP Identity)
 - user roles with different permissions (admin)
 - asynchronous validation forms (client-side) (Microsoft.jQuery.Unobtrusive.Validation)
 - data models with attribute-based validation (DataAnnotations)
 - user data management (change of data / password)
 - displaying order history for each user
 - order status change (administrator role required)
 - adding a new product (including photos) (administrator role required)
 - edition of existing products (administrator role required)

**Summary:**
  - Entity Framework (CodeFirst), Bootstrap, RWD, Cache, ASP Identity, Linq, JavaScript, Ajax, jQuery, MvcSiteMapProvider, Unobtrusive Validation, and a lot more...
  
  # Screenshots:
Screenshot 1. Product catalog with asynchronous search engine (ajax):
![](Screenshots/screenshot%20(2).png)

Screenshot 2. Cart:
![](Screenshots/screenshot%20(3).png)

Screenshot 3. User data management and password change:
![](Screenshots/screenshot%20(4).png)

Screenshot 4. Order history / changing order status (admin role required):
![](Screenshots/screenshot%20(5).png)

Screenshot 5. Product adding / editing (admin role required):
![](Screenshots/screenshot%20(6).png)

# Todos

 - Add Night Mode

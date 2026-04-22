<img width="1280" height="720" alt="gluten_logo" src="https://github.com/user-attachments/assets/48aee382-685c-494e-a1e1-0b763373bd12" />

This MAUI application aims to present fast information if a grocery/ware may contain gluten.

The application is not completed and should not be used to determine if a food is 100% safe to consume if user suffers from celiac, validate the contents at your own disclosure.

Main Use-Case is when the ingredients list text font is way to small, this would help remedy that hurdle.

Celiac is a autoimmune disease that targets the bodys function to break down wheat protein, which is quite common in the Nordics. I'm not gluten, but half my relatives are. 

As of right now, it uses the API provided from Open Food Facts. I parse and validate from the allergen sections, as of now "allergens" and "allergens_from_ingredients". This will be expanded upon.

The data is user submitted, it can contain wrong or incorrect data. 

Here's an example of a gluten free ware, McVites Chocolate Biscuits: https://world.openfoodfacts.net/api/v3/product/5000168195162

And here's a ware with gluten Karlssons Fyrkant: https://world.openfoodfacts.net/api/v3/product/7311071330525

Example video of use case: https://www.youtube.com/shorts/j89ODCauhkI

Application is built in according to Onion Architecture, seperating layers of concern from UI to Domain level.

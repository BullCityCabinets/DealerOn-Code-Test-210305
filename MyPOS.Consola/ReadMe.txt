03/05/2021

Thank you for considering me for DealerOn!

  I used this project as an opportunity to explore more about the utility of interfaces to keep my code less coupled.  I had previously built a very UI heavy app, and it was interesting to try to maintain separation-of-concerns in a console app.  I intend to, beyond completing the task, illustrate my ability to plan extendible, maintainable code.

  User input syntax is modelled on the assignment’s layout, but I hope you see I’ve prepared for expansion, world-wide.  Custom input is validated to ensure uniformity in service.  Unit tests are included and modelled on your input parameters.

    I would also enjoy sharing my Xamarin app, if you would like to see how I approached the MVVM pattern.  I found this to be a gratifying test with some tasks I'd not investigated thoroughly.  I look forward to your critique!
  

Tom Grossi
919-500-1394
ScumSprocket@Gmail.com



For your convenience, this is a copy of the test's specified inputs:

INPUT 1
1 Book at 12.49
1 Book at 12.49
1 Music CD at 14.99
1 Chocolate bar at 0.85

OUTPUT 1
Book: 24.98 (2 @ 12.49)
Music CD: 16.49
Chocolate bar: 0.85
Sales Taxes: 1.50
Total: 42.32


INPUT 2
1 Imported box of chocolates at 10.00
1 Imported bottle of perfume at 47.50

OUTPUT 2
Imported box of chocolates: 10.50
Imported bottle of perfume: 54.65
Sales Taxes: 7.65
Total: 65.15

INPUT 3
1 Imported bottle of perfume at 27.99
1 Bottle of perfume at 18.99
1 Packet of headache pills at 9.75
1 Imported box of chocolates at 11.25
1 Imported box of chocolates at 11.25

OUTPUT 3
Imported bottle of perfume: 32.19
Bottle of perfume: 20.89
Packet of headache pills: 9.75
Imported box of chocolates: 23.70 (2 @ 11.85)
Sales Taxes: 7.30
Total: 86.53
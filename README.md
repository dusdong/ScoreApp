Awesome ScoreApp
========
## Frameworks
* [AngularJS](http://angularjs.org/)
 * [Fundamentals](http://pluralsight.com/training/Courses/TableOfContents/angularjs-fundamentals)
 * [Best Practices](http://pluralsight.com/training/Courses/TableOfContents/angular-best-practices)
* [Asp.Net WebApi](http://www.asp.net/web-api)
* [Bootstrap](http://getbootstrap.com)
* [UserApp](https://www.userapp.io/)
 * [Social Login](https://app.userapp.io/#/docs/concepts/#social-login)

## Libraries
* [angular-ui](http://angular-ui.github.io/bootstrap/)
* [moment](http://momentjs.com/)
* [angular-moment](https://github.com/urish/angular-moment)
* [underscore](http://underscorejs.org/)
* [userapp-angular](https://github.com/userapp-io/userapp-angular)
* [angular-toaster](https://github.com/jirikavi/AngularJS-Toaster)
* [angular-busy](https://github.com/cgross/angular-busy)

## Development
In order to run the project in development environment, a few actions must be taken:

1. Clone the repository.
2. Configure Local IIS.
 * Add Web Site.
 * Give it a name.
 * Set the *Physical Path* to point the folder *ScoreApp.UI*, which is under your repository path.
 * Type *awesomescoreapp.com* in the *Host name* field.
3. Add a mapping to *hosts* file at *C:\Windows\System32\drivers\etc\hosts*:

        127.0.0.1	awesomescoreapp.com
4. Run scripts in *MySQL* local instance.

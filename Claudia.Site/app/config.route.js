(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());
    
    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: '../app/dashboard/dashboard.html',
                    title: 'dashboard',
                    settings: {
                        nav: 1,
                        content: 'Dashboard', //<i class="fa fa-dashboard"></i> 
                        icon: 'fa fa-dashboard'
                    }
                }
            }, {
                url: '/admin',
                config: {
                    title: 'admin',
                    templateUrl: '../app/admin/admin.html',
                    settings: {
                        //nav: 2,
                        content: 'Admin', //<i class="fa fa-lock"></i> 
                        icon: 'fa fa-lock'
                    }
                }
            }, {
                url: '/gallery',
                config: {
                    title: 'gallery',
                    templateUrl: '../app/gallery/gallery.html',
                    settings: {
                        nav: 3,
                        content: 'Gallery', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-picture-o'
                    }
                }
            }, {
                url: '/youtube',
                config: {
                    title: 'youtube',
                    templateUrl: '../app/youtube/youtube.html',
                    settings: {
                        nav: 4,
                        content: 'YouTube', //<i class="fa fa-youtube"></i> 
                        icon: 'fa fa-youtube'
                    }
                }
            }, {
                url: '/carousel',
                config: {
                    title: 'carousel',
                    templateUrl: '../app/carousel/carousel.html',
                    settings: {
                        nav: 5,
                        content: 'Carousel', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-picture-o'
                    }
                }
            }, {
                url: '/recipe',
                config: {
                    title: 'recipe',
                    templateUrl: '../app/recipe/recipe.html',
                    settings: {
                        nav: 6,
                        content: 'Recipe', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-file-text-o'
                    }
                }
            }, {
                url: '/comments',
                config: {
                    title: 'comments',
                    templateUrl: '../app/comments/comments.html',
                    settings: {
                        //nav: 6,
                        content: 'Comments', //<i class="fa fa-comments"></i> 
                        icon: 'fa fa-comments'
                    }
                }
            }, {
                url: '/gallery/:id',
                config: {
                    title: 'gallery-detail',
                    templateUrl: '../app/gallery/gallerydetail.html',
                    settings: {
                        content: 'Gallery', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-picture-o'
                    }
                }
            }, {
                url: '/youtube/:id',
                config: {
                    title: 'youtube-detail',
                    templateUrl: '../app/youtube/youtubedetail.html',
                    settings: {
                        content: 'YouTube', //<i class="fa fa-youtube"></i> 
                        icon: 'fa fa-youtube'
                    }
                }
            }, {
                url: '/carousel/:id',
                config: {
                    title: 'carousel-detail',
                    templateUrl: '../app/carousel/carouseldetail.html',
                    settings: {
                        content: 'Carousel', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-picture-o'
                    }
                }
            }, {
                url: '/recipe/:id',
                config: {
                    title: 'recipe-detail',
                    templateUrl: '../app/recipe/recipedetail.html',
                    settings: {
                        content: 'Recipe', //<i class="fa fa-picture-o"></i> 
                        icon: 'fa fa-file-text-o'
                    }
                }
            }, {
                url: '/comment/:id',
                config: {
                    title: 'comment-detail',
                    templateUrl: '../app/comments/commentdetail.html',
                    settings: {}
                }
            }];
    }
})();
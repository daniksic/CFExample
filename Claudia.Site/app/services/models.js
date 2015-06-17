(function () {
    'use strict';

    var serviceId = 'models';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['config',models]);

    function models(config) {
        // Define the functions and properties to reveal.
        var entityNames = {
            annoucement: "Annoucement",
            askClaudia: "AskClaudia",
            comment: "Comment",
            link: "Link",
            category: "Category",
            rating: "Rating",
            recipe: "Recipe",
            recipeCategory: "RecipeCategory",
            dto : {
                link : "LinkDTO"
            }
        };

        var service = {
            configureMetadataStore: configureMetadataStore,
            entityNames: entityNames
        };

        return service;

        function configureMetadataStore(metadataStore) {
            registerLink(metadataStore);

            //registerDTO(metadataStore);
        }

        //#region Internal Methods        



        function registerLink(metadataStore) {
            metadataStore.registerEntityTypeCtor(entityNames.link, Link);

            function Link() {
            }
            Object.defineProperty(Link.prototype, 'imageurl', {
                get: function () {
                    var url;
                    switch (this.categoryId) {
                        case 1:
                            url = config.imageSettings.gallery.getImageUrl(this.serverFileName);
                            break;
                        case 2:
                            url = config.imageSettings.youtube.getImageUrl(this.serverFileName);
                            break;
                        case 6:
                            url = config.imageSettings.carousel.getImageUrl(this.serverFileName);
                            break;
                        default:
                            url = "noimage.jpg";
                            break;
                    }

                    return url;
                }
            })
            Object.defineProperty(Link.prototype, 'shortTitle', {
                get: function () {
                    var val = this.title;
                    if (val.length > 20) {
                        return val.substring(0, 20) + "...";
                    }
                    return val;
                }
            })

            function linkInit(link) {
                Object.defineProperty(link.__proto__, 'imageurl', { get: function () { return config.imageSettings.gallery.getImageUrl(this.serverFileName); } })
            }
        }

        function registerDTO(metadataStore) {
            metadataStore.registerEntityTypeCtor(entityNames.dto.link, linkDto);
        }



        function linkDto() {
            link.call(this);
            this.crop = "";
        }
        //#endregion
    }
})();
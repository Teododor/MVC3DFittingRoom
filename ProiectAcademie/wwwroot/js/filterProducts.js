function navigateToProductIndex(searchText, pgValue, selectedType) {





    window.location.href = `/Product/Index?SearchText=${searchText}&pg=${pgValue}&selectedType=${selectedType}`;




    /*    console.log("PGVALUE: " + pgValue);*/


    /*    var sortingButton = document.getElementById("SortingButton");
    
        console.log(sortingButton);
    
        fetch('/Product/Index', {
            method: 'GET',
            headers: {
                'Content-Type' : 'application/json'
            }
        })
    
            .then(response => {
                if (!(response.ok)) {
                    throw new Error(`HTTP ERROR! Stauts : ${response.status}`);
                }
    
                return response.json();
            })
    
            .then(data => { 
                console.log("The data is the following: " + data);
                const generateHTML = generateHTML(data.model, data.pager, data.CurentUser);
    
                document.getElementById('root').innerHTML = generateHTML();
            })
    
            .catch(error => {
                console.error("Error: " +  error);
            })*/
}
var selectedType;
var pgValue;
var searchValue;


document.addEventListener("DOMContentLoaded", () => {
    var sortingButton = document.getElementById("SortingButton");
    var clothesType = document.getElementById("ClothesType");



    console.log('Clothes Type: ' + clothesType);

    clothesType.addEventListener('change', () => {
        selectedType = clothesType.value;
        pgValue = clothesType.dataset.mycustomvalue;
        searchValue = document.getElementById("searchText").value
        console.log("Selected Type: " + selectedType);
        console.log("Page Value: " + pgValue);
        console.log("Search Value: " + searchValue);

        navigateToProductIndex(serachValue, pgValue, selectedType);

    });
    sortingButton.addEventListener("click", () => {
        console.log("THE SORTING BUTTON WAS CLICKED");
        const sortingButton = document.getElementById("SortingButton");

        const pgValue = sortingButton.dataset.mycustomvalue;

        const searchValue = document.getElementById("searchText").value;
        navigateToProductIndex(searchValue, pgValue, selectedType);
    });
});

/*function generateHTML(model, pager, CurrentUser) {
    let html = '';

 
    html += `
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css">
        <link rel="stylesheet" href="/css/productsIndex.css">
        <h1>Welcome To the Product Page</h1>
        <button type="button" class="btn btn-primary" onclick="location.href='/Product/FavoriteProducts'">View Favorite Products</button>
        <button type="button" class="btn btn-primary" onclick="location.href='/Product/ViewBasket'">View Basket</button>
        <div class="col-4 p-0 justify-content-end">
            <input id="txt" type="text" />
            <button id="SortingButton">Search</button>
        </div>
    `;


    html += '<div class="container"><div class="products">';

    model.Products.forEach(product => {
        const isFavorite = model.FavouriteProductIds.includes(product.Id);
        const favoriteClass = isFavorite ? "fas" : "far";
        const isAlreadyInCart = model.UserProductsIds.includes(product.Id);
        const shoppingCartElementClass = !isAlreadyInCart ? "buy-icon" : "add-icon";

        html += `
            <div class="product">
                <div class="product-info">
                    <h3>${product.Name}</h3>
                </div>
                <img class="product-image" src="/Product/DisplayProductImage?ProductId=${product.ImageId}" alt="no image has been found">
                <a>${product.Description}</a>
                <a>${product.Price}</a>
                <div class="toast custom-toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="5000" id="productToast">
                    <div class="toast-header">
                        <img src="path_to_your_icon.png" class="rounded mr-2" alt="icon">
                        <strong class="mr-auto">Notification</strong>
                        <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="toast-body">
                        Product was added to cart.
                    </div>
                </div>
        `;

        if (CurrentUser.IsAuthenticated) {
            html += `
                <i class="${favoriteClass} fa-heart heart" data-product-id="${product.Id}" data-user-id="${CurrentUser.Id}"></i>
                <i class="fas fa-comment review-btn user-comment" data-product-id="${product.Id}" data-user-id="${CurrentUser.Id}"></i>
                <i class="fa fa-thin fa-shopping-cart ${shoppingCartElementClass}" data-product-id="${product.Id}" data-user-id="${CurrentUser.Id}"></i>
            `;
        }

        html += `
                <button class="btn btn-primary">See All Reviews</button>
            </div>
        `;
    });


    html += '</div></div>';


    html += `
        <div id="reviewModal" class="modal">
            <div class="modal-content">
                <input id="ModalProductId" type="hidden" />
                <input id="ModalCommentIsNew" type="hidden" />
                <span class="close">&times;</span>
                <h2>Leave a Review</h2>
                <textarea id="WrittenTextArea" placeholder="Write your review here..." rows="4" style="width:100%;"></textarea>
                <br>
                <button class="btn btn-primary submit-button">Submit</button>
            </div>
        </div>
    `;

    return html;
}
 */

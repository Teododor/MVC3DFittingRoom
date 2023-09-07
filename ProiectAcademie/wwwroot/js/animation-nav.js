document.addEventListener("DOMContentLoaded", function () {
    let navItems = document.querySelectorAll(".animation-nav");

    navItems.forEach(function (navItem) {
        navItem.addEventListener("click", function () {
            this.classList.toggle("clicked");

            // Remove the class after the animation completes
            setTimeout(() => {
                this.classList.toggle("clicked");
            }, 300); // 300ms matches the CSS transition duration
        });
    });
});
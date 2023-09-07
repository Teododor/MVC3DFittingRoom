document.addEventListener("DOMContentLoaded", function() {
    var selectElement = document.getElementById('ChosenCountry');
    
    var selectedIndex;
    var selectedOption;
    var selectedValue;
    var selectedText;

    selectElement.addEventListener('change', function() {
         selectedIndex = selectElement.selectedIndex;
         selectedOption = selectElement.options[selectedIndex];
         selectedValue = selectedOption.value;
         selectedText = selectedOption.text;

        console.log("Selected index: ", selectedIndex);
        console.log("Selected option: ", selectedOption);
        console.log("Selected value: ", selectedValue);
        console.log("Selected text: ", selectedText);
    })
});

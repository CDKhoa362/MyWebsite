document.addEventListener("DOMContentLoaded", function () {
    let typingElement = document.querySelector(".typing");
    if (!typingElement) return;

    let text = typingElement.getAttribute("data-text") || "Developer";
    let index = 0;
    let delayAfterFinish = 2000;

    function typeEffect() {
        if (index < text.length) {
            typingElement.textContent = text.substring(0, index + 1);
            index++;
            setTimeout(typeEffect, 100);
        } else {
            setTimeout(() => {
                typingElement.textContent = "";
                index = 0;
                typeEffect();
            }, delayAfterFinish);
        }
    }

    typeEffect();
});
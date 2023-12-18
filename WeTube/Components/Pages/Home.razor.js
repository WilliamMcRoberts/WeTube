
export function onLoad() {
    console.log("Loaded / Homesssss.after..");
    animateSlideRight();
    observeSlideRight();
    let el = document.getElementById('scroll');
    window.onscroll = () => {
        console.log("Scroll Y from index.js: ", window.scrollY);
        el.style.transform = `translateY(${window.scrollY * 1.15}px)`;
    }

    processAllHtmxElements(".process-home");
}

export function onUpdate() {
    console.log("Updated / Home...");
}

export function onDispose() {
    console.log("Disposed / Home...");
}

export function onLoad() {
    console.log("Loaded / Weather...")
    animateSlideRight();

    processAllHtmxElements();
}
export function onUpdate() {
    console.log("Updated / W...")
}

export function onDispose() {
    console.log("Disposed / Weather...")
}


function processAllHtmxElements() {
    document.querySelectorAll('.process-weather').forEach(el => {
        htmx.process(el);
    });
}
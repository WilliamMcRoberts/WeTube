//import {observeSlideRight, animateSlideRight } from '/js/hooks.js';

export function onLoad() {
    console.log("Loaded / Home...");
    animateSlideRight();
    observeSlideRight();
    let el = document.getElementById('scroll');
    window.onscroll = () => {
        console.log("Scroll Y from index.js: ", window.scrollY);
        el.style.transform = `translateY(${window.scrollY * 1.15}px)`;
    }

    processAllHtmxElements();
}

export function onUpdate() {
    console.log("Updated / Home...");
}

export function onDispose() {
    console.log("Disposed / Home...");
}


function processAllHtmxElements() {
    document.querySelectorAll('.process-home').forEach(el => {
        htmx.process(el);
    });
}
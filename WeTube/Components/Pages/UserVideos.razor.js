export function onLoad() {
    console.log("Loaded / UserVideos.CHeck after..")
    const vids = document.querySelectorAll("#my-video");
    vids.forEach(vid => {
        console.log("Processing Vid: ", vid);
        videojs(vid);
    });
    processAllHtmxElements(".process-videos");
}
export function onUpdate() {
    console.log("Updated / WUserVideos");
}

export function onDispose() {
    console.log("Disposed / UserVideos...")
}
const canvas = document.getElementById('rick-canvas');
const ctx = canvas.getContext('2d');
canvas.width = 1200;
canvas.height = 900;

const keys = [];

const player = {
    x: 400,
    y: 200,
    width: 129,
    height: 166,
    frameX: 0,
    frameY: 0,
    speed: 9,
    moving: false
};

const playerSprite = new Image();
playerSprite.src = "/Images/rickSprite.png";

const background = new Image();
background.src = "/Images/background.jpg";

function drawSprite(img, sX, sY, sW, sH, dX, dY, dW, dH) {  // Source / Destination
    ctx.drawImage(img, sX, sY, sW, sH, dX, dY, dW, dH);
}


window.addEventListener('keydown', (e) => {
    keys.push(e.key);
    player.moving = true;
});

window.addEventListener('keyup', (e) => {
    keys.length = 0;
    player.moving = false;
});

function movePlayer() {
    if (keys.includes('ArrowUp') && player.y > 0) {
        player.y -= player.speed;
        player.frameY = 3;
        player.moving = true;
    }
    if (keys.includes('ArrowDown') && player.y < (canvas.height - player.height)) {
        player.y += player.speed;
        player.frameY = 0;
        player.moving = true;
    }
    if (keys.includes('ArrowRight') && player.x < (canvas.width - player.width)) {
        player.x += player.speed;
        player.frameY = 2;
        player.moving = true;
    }
    if (keys.includes('ArrowLeft') && player.x > 0) {
        player.x -= player.speed;
        player.frameY = 1;
        player.moving = true;
    }
}

function handlePlayerFrame() {
    if (player.frameX < 3 && player.moving) {
        player.frameX++;
    }
    else player.frameX = 0;
}


let fps, fpsInterval, startTime, now, then, elapsed;

startAnimating = (fps) => {
    fpsInterval = 1000 / fps;
    then = Date.now();
    startTime = then;
            animate();
}

animate = () => {
    requestAnimationFrame(animate);
    now = Date.now();
    elapsed = now - then;
    if (elapsed > fpsInterval) {
        then = now - (elapsed % fpsInterval);
        ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(background, 0, 0, canvas.width, canvas.height);
        
            drawSprite(playerSprite, player.width * player.frameX, player.height * player.frameY, player.width, player.height, player.x, player.y, player.width, player.height)
        
        movePlayer();
        handlePlayerFrame();
        requestAnimationFrame(animate);
    }
}
    startAnimating(60);

const width = 600;
const height = 600;

const canvas = document.getElementsByTagName("canvas")[0];
canvas.width = width;
canvas.height = height;

const context = canvas.getContext("2d");

const backColor = "skyblue";
const pixelSize = 1;

function toDisplayCoordinates(x, y) {
    x = width / 2 + x;
    y = height / 2 - y;
    return [x, y];
}

function setPixel(x, y, color) {
    [x, y] = toDisplayCoordinates(x, y);

    context.fillStyle = color;
    context.fillRect(x, y, pixelSize, pixelSize);
}

centerX = 0;
centerY = 0;
radius = 100 * pixelSize;
size = 100 * pixelSize;

const firstColor = "black";
const secondColor = "white";
const sectorSize = 10 * pixelSize;

function div(val, by) {
    return (val - val % by) / by;
}

function getColorCircle(x, y) {
    const number = radius ** 2 - ((x - centerX) ** 2 + (y - centerY) ** 2);
    if (!(number >= 0 && number <= 200))
        return backColor;

    return getChessColor(x, y);
}

function getColorRect(x, y) {
    if (x > centerX + size / 2 || x < centerX - size / 2 || y > centerY + size / 2 || y < centerY - size / 2)
        return backColor;

    return getChessColor(x, y);
}

function getChessColor(x, y) {
    x -= centerX;
    y -= centerY;

    const colorXIndex = div(x, sectorSize) % 2;
    const colorYIndex = div(y, sectorSize) % 2;

    const indexIsEven = Math.abs(colorXIndex) === Math.abs(colorYIndex);

    if (indexIsEven && (y >= 0 && x >= 0 || y < 0 && x < 0) ||
        !indexIsEven && (y >= 0 && x < 0 || y < 0 && x >= 0))
        return firstColor;
    else
        return secondColor;
}

async function drawScene() {
    return new Promise(resolve => {
        for (let y = height / 2; y > -height / 2; y -= pixelSize) {
            for (let x = -width / 2; x < width / 2; x += pixelSize) {
                const color = getColorCircle(x, y);
                setPixel(x, y, color);
            }
        }
        resolve();
    });
}

drawScene();

const DotProduct = function (v1, v2) {
    return v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];
}

const Length = function (vec) {
    return Math.sqrt(DotProduct(vec, vec));
}

const MultiplySV = function (k, vec) {
    return [k * vec[0], k * vec[1], k * vec[2]];
}

const Normalize = (vec) => {
    return MultiplySV((1 / Length(vec)), vec);
}

function getAngle(vec1, vec2) {
    // const dotProduct = DotProduct(vec1, vec2);
    // const len1 = Length(vec1);
    // const len2 = Length(vec2);
    // const cosA = dotProduct / (len1 * len2);

    // Math.Atan2(b.Y - a.Y,b.X - a.X);
    return Math.atan2(vec2[1] - vec1[1], vec2[0] - vec1[0]);
}

function drawLine(fromX, fromY, toX, toY) {
    if (toX === undefined || toY === undefined) {
        toX = fromX;
        toY = fromY;
        fromX = 0;
        fromY = 0;
    }

    [fromX, fromY] = toDisplayCoordinates(fromX, fromY);
    [toX, toY] = toDisplayCoordinates(toX, toY);
    context.beginPath();
    context.moveTo(fromX, fromY);
    context.lineTo(toX, toY);
    context.stroke();
}

context.strokeStyle = "red";
drawLine(centerX + radius, centerY);

function drawAngle(x, y) {
    const vec = Normalize([x, y, 0]);
    const zeroVec = Normalize([centerX + radius, centerY, 0]);
    const angle = getAngle(vec, zeroVec) * (Math.sign(y) || 1);
    console.log(angle / Math.PI * 180);

    const circleSize = 2 * Math.PI * radius;
    const sectorSize = angle * radius;
    console.log(`${sectorSize} / ${circleSize} = ${sectorSize / circleSize}`)

    context.strokeStyle = "blue";
    drawLine(x, y);
}

drawAngle(centerX, centerY + radius);

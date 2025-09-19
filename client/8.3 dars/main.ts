for (let i = 0; i < 3; i++) {
  console.log(i);
}

let i: number = 0;
while (i < 3) {
  console.log(i);
  i++;
}

const fruits = ["apple", "banana", "cherry"];

for (let fruit of fruits) {
  console.log(fruit);
}

for (let index in fruits) {
  console.log(index);       // index
  console.log(fruits[index]); // value
}
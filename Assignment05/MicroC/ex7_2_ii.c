void arrsum(int n, int arr[], int *sump){
  int i;
  i = 0;

  while (i < n) {
    *sump = *sump + arr[i];
    i = i + 1;
  }
}

// Exercise 7.2 (ii)
void squares(int n, int arr[]){
  int i;
  i = 0;

  while (i < n) {
    arr[i] = i * i;
    i = i + 1;
  }
}

void main(int n) { 
    int sum;
    sum = 0;
    int arr[20];

    squares(20, arr);
    arrsum(n, arr, &sum);
    print sum;
}

void arrsum(int n, int arr[], int *sump){
  int i;  
  for (i = 0; i < n; i = i + 1) *sump = *sump + arr[i];
}

// Exercise 7.3 (ii)
void squares(int n, int arr[]){
  int i;
  for (i = 0; i < n; i = i + 1) arr[i] = i * i;
}

void main(int n) { 
    int sum;
    sum = 0;
    int arr[20];

    squares(20, arr);
    arrsum(n, arr, &sum);
    print sum;
}

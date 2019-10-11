// Exercise 7.3 (i)
void arrsum(int n, int arr[], int *sump){
  int i;  
  for(i = 0; i < n; i = i + 1) *sump = *sump + arr[i];
}

void main(int n) { 
    int sum;
    sum = 0;
    int arr[4];
  
    arr[0] = 7;
    arr[1] = 13;
    arr[2] = 9;
    arr[3] = 8;

    arrsum(n, arr, &sum);
    print sum;
}

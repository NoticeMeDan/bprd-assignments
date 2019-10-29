// Exercise 7.3 (iii)
void histogram(int n, int ns[], int max, int freq[]){
  int i;

  for (i = 0; i < n; i = i + 1) {
    int j;
    j = ns[i];
    
    if (j <= max) freq[j] = freq[j] + 1;
  }
}

void main(int n) { 
  int i;
  int j;
  int arr[7];
  int freq[100];

  arr[0] = 1;
  arr[1] = 2;
  arr[2] = 1;
  arr[3] = 1;
  arr[4] = 1;
  arr[5] = 2;
  arr[6] = 0;
  
  for (i = 0; i < n; i = i + 1) freq[i] = 0;

  histogram(7, arr, n, freq);
  
  for (j = 0; j < n; j = j + 1) print freq[j];
}

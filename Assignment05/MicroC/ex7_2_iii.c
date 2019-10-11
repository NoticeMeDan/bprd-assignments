// Exercise 7.2 (iIi)
void histogram(int n, int ns[], int max, int freq[]){
  int i;
  i = 0;

  while (i < n) {
    int j;
    j = ns[i];

    if (j <= max) freq[j] = freq[j] + 1;
    i = i + 1;
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

    i = 0;
    while (i < n){
      freq[i] = 0;
      i = i + 1;
    }

    histogram(7, arr, n, freq);
    
    j = 0;
    while (j < n) {
      print freq[j];
      j = j + 1;
    }
}

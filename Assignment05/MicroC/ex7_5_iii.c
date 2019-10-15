int a[20];			/* Must be global */

void main(int n) { 
  int i; 
  int f; 
  f = 0;
  int max;
  max = 7;
  for (i=0; i < n; i=i+1) {
    a[i] = f;
    i = i + 1;
    f = f+i;
    if (f>=max)
      f=0;  
  }
  int freq[7];
  histogram (n, a, max, freq);
  for (i=0; i < max; ++i){
    print freq[i];
  }
}

void histogram(int n, int ns[], int max, int freq[]){
  int x;
 for (x=0; x < max; ++x){
    freq[x] = 0;
  }
  int i;
  for (i=0; i < n; ++i){
    freq[ns[i]] = freq[ns[i]] + 1;
  }
}

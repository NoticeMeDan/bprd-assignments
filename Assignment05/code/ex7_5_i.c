int a[20];			/* Must be global */

void main(int n) { 
  int i; 
  int f; 
  f = 1;
  for (i=0; i < n; ++i) {
    a[i] = f;
    f = f + f;
  }
  int res;
  arrsum(n, a, &res);
  print res;
}


void arrsum (int n, int arr[], int *sump){
  int i;
  *sump = 0;
  for (i=0; i < n; ++i){
    *sump = *sump + arr[i];
  }
  print *sump;
}
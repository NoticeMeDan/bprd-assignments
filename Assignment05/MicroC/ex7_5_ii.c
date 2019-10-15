int a[20];			/* Must be global */

void main(int n) { 
  squares(n, a);
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
}

void squares (int n, int arr[]){
  int i;
  for (i=0; i < n; ++i){
    arr[i] = i*i;
  }
}
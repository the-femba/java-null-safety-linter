void bar(@NotNull Point p) {
    p = null; // warning
    
    a = 1; // error
    
    int a = 3;
    a = null; // warning
    
    @Nullable int b = 4;
    b = null; // ok
    
    func(a); // warning
    func(b); // ok
    funcNotExists(notExistsVar); // hint
}

void func(@NotNull Point p) { }
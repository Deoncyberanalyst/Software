;Hello world.asm

section .data
    msg     db      "Hello world!",0

section .bss

section .text
    global main
main:   
    mov     rax,1
    mov     rdi,1
    mov     rsi,msg
    mov     rdx,12
    syscall
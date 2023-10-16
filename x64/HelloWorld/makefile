#This is the makefile for helloworld.asm

hello:	hello.o
	gcc -o hello.o -no-pie

hello.o: helloworld.asm
	nasm -f elf64 -g -F dwarf hello.asm -l hello.lst
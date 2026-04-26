import { cva, type VariantProps } from "class-variance-authority";

export const alertVariants = cva(
  "relative w-full rounded-2xl border px-4 py-3 text-sm [&>svg]:absolute [&>svg]:left-4 [&>svg]:top-4 [&>svg~*]:pl-7",
  {
    variants: {
      variant: {
        default: "border-border/70 bg-background/80 text-foreground",
        destructive: "border-red-200 bg-red-50 text-red-700",
      },
    },
    defaultVariants: {
      variant: "default",
    },
  },
);

export type AlertVariants = VariantProps<typeof alertVariants>;

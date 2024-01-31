import { Grid } from "@mui/material";
import { Product } from "../../models/product";
import ProductCard from "./ProductCard";
import { UseAppSelector } from "../../app/store/configureStore";
import ProductCardSkeleton from "./ProductCardComponent";

interface Props {
  products: Product[];
}

export default function ProductList({products} : Props) {
  const {productsLoaded} = UseAppSelector(state => state.catalog);
  return(
    <Grid container spacing={4}>
        {products.map(product => (
          <Grid item xs={4} key={product.id}>
            {!productsLoaded ? (
              <ProductCardSkeleton />
            ) : (
              <ProductCard product={product} />
            )}
          </Grid>
        ))}
      </Grid>
  )
}
import { Box, Typography, Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableRow } from "@mui/material";
import { BasketItem } from "../../models/basket";
import { Order } from "../../models/order";
import BasketTable from "../basket/BasketTable";
import { currencyFormat } from "../../app/util/util";

interface Props {
  order: Order;
  setSelectedOrder: (id: number) => void;
}

export default function OrderDetailed({ order, setSelectedOrder }: Props) {
  const subtotal = order.orderItems.reduce((sum, item) => sum + (item.quantity * item.price), 0) ?? 0;
  const deliveryFee = subtotal > 10000 ? 0 : 500;
  return (
      <>
          <Box display='flex' justifyContent='space-between'>
              <Typography sx={{ p: 2 }} gutterBottom variant='h4'>Order# {order.id} - {order.orderStatus}</Typography>
              <Button onClick={() => setSelectedOrder(0)} sx={{ m: 2 }} size='large' variant='contained'>Back to orders</Button>
          </Box>
          <BasketTable items={order.orderItems as BasketItem[]} isBasket={false} />
          <Grid container>
              <Grid item xs={6} />
              <Grid item xs={6}>
              <TableContainer component={Paper} variant={'outlined'}>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell colSpan={2}>Subtotal</TableCell>
                            <TableCell align="right">{currencyFormat(subtotal)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Delivery fee*</TableCell>
                            <TableCell align="right">{currencyFormat(deliveryFee)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Total</TableCell>
                            <TableCell align="right">{currencyFormat(subtotal + deliveryFee)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <span style={{fontStyle: 'italic'}}>*Orders over $100 qualify for free delivery</span>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
              </Grid>
          </Grid>
      </>
  )
}